import 'package:dio/dio.dart';
import 'package:mobile/config/app_config.dart';
import 'package:mobile/interceptors/auth_interceptor.dart';

class ApiService {
  static final Dio dio = Dio(
    BaseOptions(
      baseUrl: AppConfig.baseUrl,
      connectTimeout: Duration(seconds: 5),
      receiveTimeout: Duration(seconds: 5),
    ),
  );

  static void Initialize() {
    dio.interceptors.add(AuthInterceptor());
  }
}
