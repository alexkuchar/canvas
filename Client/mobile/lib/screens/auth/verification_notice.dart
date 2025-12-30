import 'package:flutter/material.dart';
import 'package:mobile/constants/spacing.dart';
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

  @override
  Widget build(BuildContext context) {
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
                        onPressed: () {},
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
