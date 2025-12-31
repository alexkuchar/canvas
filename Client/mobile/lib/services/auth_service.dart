import 'package:dio/dio.dart';
import 'package:mobile/config/app_config.dart';
import 'package:mobile/utils/error_handler.dart';

class AuthException implements Exception {
  final String message;
  AuthException(this.message);

  @override
  String toString() => message;
}

class AuthService {
  static Future<void> register(
    String firstName,
    String lastName,
    String email,
    String password,
  ) async {
    try {
      final response = await Dio().post(
        "${AppConfig.baseUrl}/api/auth/register",
        data: {
          "firstName": firstName,
          "lastName": lastName,
          "email": email,
          "password": password,
        },
        options: Options(validateStatus: (status) => status! < 500),
      );

      if (response.statusCode != null &&
          response.statusCode! >= 200 &&
          response.statusCode! < 300) {
        return;
      }

      final errorMessage = ErrorHandler.parseErrorMessage(
        DioException(
          requestOptions: response.requestOptions,
          response: response,
          type: DioExceptionType.badResponse,
        ),
      );
      final exception = AuthException(errorMessage);
      ErrorHandler.showErrorSnackBar(errorMessage);
      throw exception;
    } on DioException catch (error) {
      final errorMessage = ErrorHandler.parseErrorMessage(error);
      final exception = AuthException(errorMessage);
      ErrorHandler.showErrorSnackBar(errorMessage);
      throw exception;
    } catch (error) {
      final errorMessage = error is AuthException
          ? error.message
          : 'An unexpected error occurred. Please try again.';
      final exception = AuthException(errorMessage);
      ErrorHandler.showErrorSnackBar(errorMessage);
      throw exception;
    }
  }

  static Future<void> resendVerificationEmail(String email) async {
    try {
      final response = await Dio().post(
        "${AppConfig.baseUrl}/api/auth/resend-verification-email",
        data: {"email": email},
        options: Options(validateStatus: (status) => status! < 500),
      );

      if (response.statusCode != null &&
          response.statusCode! >= 200 &&
          response.statusCode! < 300) {
        return;
      }

      final errorMessage = ErrorHandler.parseErrorMessage(
        DioException(
          requestOptions: response.requestOptions,
          response: response,
          type: DioExceptionType.badResponse,
        ),
      );
      final exception = AuthException(errorMessage);
      ErrorHandler.showErrorSnackBar(errorMessage);
      throw exception;
    } on DioException catch (error) {
      final errorMessage = ErrorHandler.parseErrorMessage(error);
      final exception = AuthException(errorMessage);
      ErrorHandler.showErrorSnackBar(errorMessage);
      throw exception;
    } catch (error) {
      final errorMessage = error is AuthException
          ? error.message
          : 'An unexpected error occurred. Please try again.';
      final exception = AuthException(errorMessage);
      ErrorHandler.showErrorSnackBar(errorMessage);
      throw exception;
    }
  }
}
