import 'package:dio/dio.dart';
import 'package:mobile/services/api_service.dart';
import 'package:mobile/services/storage_service.dart';

class AuthException implements Exception {
  final String message;
  AuthException(this.message);

  @override
  String toString() => message;
}

class AuthService {
  static String _extractErrorMessage(dynamic error) {
    if (error is DioException) {
      if (error.response != null) {
        final responseData = error.response!.data;

        String? errorMessage;
        if (responseData is Map<String, dynamic>) {
          final errorObj = responseData['error'];
          if (errorObj is Map<String, dynamic>) {
            errorMessage = errorObj['message'] as String?;
          }

          if (errorMessage == null || errorMessage.isEmpty) {
            errorMessage =
                responseData['message'] as String? ??
                responseData['error'] as String? ??
                responseData['title'] as String?;
          }
        } else if (responseData is String) {
          errorMessage = responseData;
        }

        if (errorMessage != null && errorMessage.isNotEmpty) {
          return errorMessage;
        }

        // Fallback to status code based messages
        final statusCode = error.response!.statusCode;
        switch (statusCode) {
          case 400:
            return 'Invalid request. Please check your input and try again.';
          case 401:
            return 'Authentication failed. Please check your credentials.';
          case 403:
            return 'Access denied. You do not have permission to perform this action.';
          case 404:
            return 'The requested resource was not found.';
          case 409:
            return 'An account with this email already exists.';
          case 422:
            return 'Validation error. Please check your input.';
          case 500:
            return 'Server error. Please try again later.';
          default:
            return 'An error occurred (${statusCode}). Please try again.';
        }
      }

      // Handle network errors
      if (error.type == DioExceptionType.connectionTimeout ||
          error.type == DioExceptionType.sendTimeout ||
          error.type == DioExceptionType.receiveTimeout) {
        return 'Connection timeout. Please check your internet connection and try again.';
      }

      if (error.type == DioExceptionType.connectionError) {
        return 'Unable to connect to the server. Please check your internet connection.';
      }
    }

    return 'An unexpected error occurred. Please try again.';
  }

  static Future<void> register(
    String firstName,
    String lastName,
    String email,
    String password,
  ) async {
    try {
      final response = await ApiService.dio.post(
        "/api/auth/register",
        data: {
          "firstName": firstName,
          "lastName": lastName,
          "email": email,
          "password": password,
        },
      );

      if (response.statusCode == 200) {
        return;
      }

      // Handle non-200 responses
      final responseData = response.data;
      String? errorMessage;
      if (responseData is Map<String, dynamic>) {
        final errorObj = responseData['error'];
        if (errorObj is Map<String, dynamic>) {
          errorMessage = errorObj['message'] as String?;
        }
        if (errorMessage == null || errorMessage.isEmpty) {
          errorMessage = responseData['message'] as String?;
        }
      }
      throw AuthException(
        errorMessage ?? 'Registration failed. Please try again.',
      );
    } catch (error) {
      if (error is AuthException) {
        rethrow;
      }
      throw AuthException(_extractErrorMessage(error));
    }
  }

  static Future<void> resendVerificationEmail(String email) async {
    try {
      final response = await ApiService.dio.post(
        "/api/auth/resend-verification-email",
        data: {"email": email},
      );

      if (response.statusCode == 200) {
        return;
      }

      // Handle non-200 responses
      final responseData = response.data;
      String? errorMessage;
      if (responseData is Map<String, dynamic>) {
        final errorObj = responseData['error'];
        if (errorObj is Map<String, dynamic>) {
          errorMessage = errorObj['message'] as String?;
        }
        if (errorMessage == null || errorMessage.isEmpty) {
          errorMessage = responseData['message'] as String?;
        }
      }
      throw AuthException(
        errorMessage ??
            'Failed to resend verification email. Please try again.',
      );
    } catch (error) {
      if (error is AuthException) {
        rethrow;
      }
      throw AuthException(_extractErrorMessage(error));
    }
  }

  static Future<void> login(String email, String password) async {
    try {
      final response = await ApiService.dio.post(
        "/api/auth/login",
        data: {"email": email, "password": password},
      );

      if (response.statusCode == 200) {
        await StorageService.setAccessToken(
          response.data["tokens"]["accessToken"],
        );
        await StorageService.setRefreshToken(
          response.data["tokens"]["refreshToken"],
        );

        await StorageService.setFullName(
          "${response.data["user"]["firstName"]} ${response.data["user"]["lastName"]}",
        );
        await StorageService.setEmail(response.data["user"]["email"]);
        await StorageService.setUserId(response.data["user"]["id"].toString());

        return;
      }

      // Handle non-200 responses
      final responseData = response.data;
      String? errorMessage;
      if (responseData is Map<String, dynamic>) {
        final errorObj = responseData['error'];
        if (errorObj is Map<String, dynamic>) {
          errorMessage = errorObj['message'] as String?;
        }
        if (errorMessage == null || errorMessage.isEmpty) {
          errorMessage = responseData['message'] as String?;
        }
      } else if (responseData is String) {
        errorMessage = responseData;
      }
      throw AuthException(
        errorMessage ?? 'Login failed. Please check your credentials.',
      );
    } catch (error) {
      if (error is AuthException) {
        rethrow;
      }
      throw AuthException(_extractErrorMessage(error));
    }
  }

  static Future<void> forgotPassword(String email) async {
    try {
      final response = await ApiService.dio.post(
        "/api/auth/forgot-password",
        data: {"email": email},
      );

      if (response.statusCode == 200) {
        return;
      }

      final responseData = response.data;
      String? errorMessage;
      if (responseData is Map<String, dynamic>) {
        final errorObj = responseData['error'];
        if (errorObj is Map<String, dynamic>) {
          errorMessage = errorObj['message'] as String?;
        }
      }
      throw AuthException(
        errorMessage ?? 'Something went wrong. Please try again.',
      );
    } catch (error) {
      if (error is AuthException) {
        rethrow;
      }
      throw AuthException(_extractErrorMessage(error));
    }
  }
}
