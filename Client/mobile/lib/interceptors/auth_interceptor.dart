import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:mobile/config/app_config.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/storage_service.dart';

class AuthInterceptor extends Interceptor {
  @override
  Future<void> onRequest(
    RequestOptions options,
    RequestInterceptorHandler handler,
  ) async {
    final accessToken = await StorageService.getAccessToken();
    if (accessToken != null) {
      options.headers["Authorization"] = "Bearer $accessToken";
    }
    return handler.next(options);
  }

  @override
  Future<void> onError(
    DioException err,
    ErrorInterceptorHandler handler,
  ) async {
    if (err.response?.statusCode == 401) {
      final refreshToken = await StorageService.getRefreshToken();
      if (refreshToken != null) {
        try {
          final response = await Dio().post(
            "${AppConfig.baseUrl}/api/auth/refresh",
            data: {"refreshToken": refreshToken},
          );

          if (response.statusCode == 200) {
            await StorageService.setAccessToken(response.data["accessToken"]);
            await StorageService.setRefreshToken(response.data["refreshToken"]);

            final opts = err.requestOptions;
            opts.headers["Authorization"] =
                "Bearer ${response.data["accessToken"]}";

            final retryResponse = await Dio().fetch(opts);
            return handler.resolve(retryResponse);
          }
        } catch (error) {
          await StorageService.clearTokens();
          final context = navigatorKey.currentContext;
          if (context != null) {
            Navigator.pushNamedAndRemoveUntil(
              context,
              '/login',
              (route) => false,
            );
          }
        }
      }
    }
    return super.onError(err, handler);
  }
}
