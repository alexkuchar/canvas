import 'package:flutter/material.dart';
import 'package:mobile/constants/spacing.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/auth_service.dart';
import 'package:open_mail/open_mail.dart';

class VerificationNoticeScreen extends StatelessWidget {
  const VerificationNoticeScreen({super.key});

  Future<void> onOpenEmailAppPressed(BuildContext context) async {
    try {
      await OpenMail.openMailApp();
    } catch (e) {
      if (context.mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text(
              'Email app not available. Please check your inbox for the verification link.',
            ),
          ),
        );
      }
    }
  }

  void onResendVerificationLinkPressed(BuildContext context, String email) {
    AuthService.resendVerificationEmail(email)
        .then((_) {
          scaffoldMessengerKey.currentState?.showSnackBar(
            SnackBar(
              content: Text('Verification link resent successfully'),
              duration: const Duration(seconds: 5),
            ),
          );
        })
        .catchError((error) {
          final errorMessage = error.toString();
          scaffoldMessengerKey.currentState?.showSnackBar(
            SnackBar(
              content: Text(errorMessage),
              duration: const Duration(seconds: 5),
            ),
          );
        });
  }

  @override
  Widget build(BuildContext context) {
    final email = ModalRoute.of(context)?.settings.arguments as String;

    return Scaffold(
      body: Padding(
        padding: AppSpacing.paddingDefaultAll,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Text(
                  'Verify your email',
                  textAlign: TextAlign.center,
                  style: TextStyle(fontSize: 20, fontWeight: FontWeight.w500),
                ),
                const SizedBox(height: AppSpacing.spacingSmall),
                Text(
                  'Please check your email for a verification link.',
                  textAlign: TextAlign.center,
                  style: TextStyle(fontSize: 16),
                ),
                const SizedBox(height: AppSpacing.spacingDefault),
                Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    SizedBox(
                      width: double.infinity,
                      child: FilledButton(
                        onPressed: () =>
                            onResendVerificationLinkPressed(context, email),
                        child: Text("Resend verification link"),
                      ),
                    ),
                    const SizedBox(height: AppSpacing.spacingSmall),
                    SizedBox(
                      width: double.infinity,
                      child: TextButton(
                        onPressed: () => onOpenEmailAppPressed(context),
                        child: Text("Open email app"),
                      ),
                    ),
                  ],
                ),
              ],
            ),
            const SizedBox(height: AppSpacing.spacingDefault),
          ],
        ),
      ),
    );
  }
}
