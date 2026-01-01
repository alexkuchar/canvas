import 'package:flutter/material.dart';
import 'package:mobile/services/storage_service.dart';

class GuestGuardWidget extends StatelessWidget {
  final Widget child;
  const GuestGuardWidget({super.key, required this.child});

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

        if (snapshot.data != null && snapshot.data!.isNotEmpty) {
          WidgetsBinding.instance.addPostFrameCallback((_) {
            if (context.mounted) {
              Navigator.pushReplacementNamed(context, '/dashboard');
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
