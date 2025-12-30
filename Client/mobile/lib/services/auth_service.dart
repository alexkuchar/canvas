import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:mobile/config/app_config.dart';

class AuthService {
  static Future<void> register(
    String firstName,
    String lastName,
    String email,
    String password,
  ) async {
    final response = await http.post(
      Uri.parse("${AppConfig.baseUrl}/api/auth/register"),
      headers: {"Content-Type": "application/json"},
      body: jsonEncode({
        "firstName": firstName,
        "lastName": lastName,
        "email": email,
        "password": password,
      }),
    );

    if (response.statusCode != 201) {
      String errorMessage = 'Failed to register';

      if (response.body.isNotEmpty) {
        try {
          final errorJson = jsonDecode(response.body) as Map<String, dynamic>;

          if (errorJson.containsKey('error')) {
            final error = errorJson['error'];
            if (error is Map<String, dynamic> && error.containsKey('message')) {
              final message = error['message'] as String?;
              if (message != null && message.isNotEmpty) {
                errorMessage = message;
              }
            }
          } else if (errorJson.containsKey('message')) {
            final message = errorJson['message'] as String?;
            if (message != null && message.isNotEmpty) {
              errorMessage = message;
            }
          }
        } catch (e) {
          if (response.body.length < 500) {
            errorMessage = response.body;
          }
        }
      }

      throw Exception(errorMessage);
    }
  }
}
