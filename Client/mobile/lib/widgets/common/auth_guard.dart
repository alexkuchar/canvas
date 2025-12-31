import 'package:flutter/material.dart';
import 'package:mobile/services/storage_service.dart';

class AuthGuardWidget extends StatelessWidget {
  final Widget child;
  const AuthGuardWidget({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: StorageService.getAccessToken(),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const Scaffold(
            body: Center(child: CircularProgressIndicator()),
          );
        }

        if (snapshot.data == null || snapshot.data!.isEmpty) {
          WidgetsBinding.instance.addPostFrameCallback((_) {
            if (context.mounted) {
              Navigator.pushReplacementNamed(context, '/login');
            }
          });
          return const Scaffold(
            body: Center(child: CircularProgressIndicator()),
          );
        }

        return child;
      },
    );
  }
}
