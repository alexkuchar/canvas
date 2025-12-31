import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:mobile/main.dart';

class ErrorHandler {
  ErrorHandler._();

  static void showErrorSnackBar(String message) {
    scaffoldMessengerKey.currentState?.showSnackBar(
      SnackBar(
        content: Text(message),
        backgroundColor: Colors.red,
        duration: const Duration(seconds: 5),
        behavior: SnackBarBehavior.floating,
      ),
    );
  }

  static String parseErrorMessage(DioException error) {
    if (error.type == DioExceptionType.connectionTimeout ||
        error.type == DioExceptionType.sendTimeout ||
        error.type == DioExceptionType.receiveTimeout) {
      return 'Connection timeout. Please check your internet connection and try again.';
    }

    if (error.type == DioExceptionType.connectionError) {
      return 'Unable to connect to the server. Please check your internet connection.';
    }

    if (error.response != null) {
      final statusCode = error.response!.statusCode;
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

    return 'An unexpected error occurred. Please try again.';
  }
}
