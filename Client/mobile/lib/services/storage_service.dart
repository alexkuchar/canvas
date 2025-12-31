import 'package:flutter/services.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class StorageService {
  static const _storage = FlutterSecureStorage();

  static Future<String?> getAccessToken() async {
    try {
      return await _storage.read(key: "access_token");
    } on MissingPluginException {
      return null;
    } catch (e) {
      return null;
    }
  }

  static Future<String?> getRefreshToken() async {
    try {
      return await _storage.read(key: "refresh_token");
    } on MissingPluginException {
      return null;
    } catch (e) {
      return null;
    }
  }

  static Future<String?> getFullName() async {
    try {
      return await _storage.read(key: "full_name");
    } on MissingPluginException {
      return null;
    } catch (e) {
      return null;
    }
  }

  static Future<String?> getEmail() async {
    try {
      return await _storage.read(key: "email");
    } on MissingPluginException {
      return null;
    } catch (e) {
      return null;
    }
  }

  static Future<String?> getUserId() async {
    try {
      return await _storage.read(key: "user_id");
    } on MissingPluginException {
      return null;
    } catch (e) {
      return null;
    }
  }

  static Future<void> setAccessToken(String accessToken) async {
    try {
      await _storage.write(key: "access_token", value: accessToken);
    } on MissingPluginException {
      return;
    } catch (e) {
      return;
    }
  }

  static Future<void> setRefreshToken(String refreshToken) async {
    try {
      await _storage.write(key: "refresh_token", value: refreshToken);
    } on MissingPluginException {
      // Plugin not available, silently fail
    } catch (e) {
      // Ignore other errors
    }
  }

  static Future<void> setFullName(String fullName) async {
    try {
      await _storage.write(key: "full_name", value: fullName);
    } on MissingPluginException {
      // Plugin not available, silently fail
    } catch (e) {
      // Ignore other errors
    }
  }

  static Future<void> setEmail(String email) async {
    try {
      await _storage.write(key: "email", value: email);
    } on MissingPluginException {
      // Plugin not available, silently fail
    } catch (e) {
      // Ignore other errors
    }
  }

  static Future<void> setUserId(String userId) async {
    try {
      await _storage.write(key: "user_id", value: userId);
    } on MissingPluginException {
      // Plugin not available, silently fail
    } catch (e) {
      // Ignore other errors
    }
  }

  static Future<void> clearTokens() async {
    try {
      await _storage.delete(key: "access_token");
      await _storage.delete(key: "refresh_token");
    } on MissingPluginException {
      // Plugin not available, silently fail
    } catch (e) {
      // Ignore other errors
    }
  }
}
