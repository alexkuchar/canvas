import 'package:flutter/material.dart';
import 'package:mobile/constants/spacing.dart';
import 'package:mobile/services/auth_service.dart';
import 'package:mobile/utils/error_handler.dart';

class ForgotPasswordScreen extends StatelessWidget {
  const ForgotPasswordScreen({super.key});

  void onContinuePressed(BuildContext context, String email) {
    AuthService.forgotPassword(email)
        .then((_) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
              content: Text('Password reset email sent'),
              duration: Duration(seconds: 5),
            ),
          );
          Navigator.pushReplacementNamed(context, '/login');
        })
        .catchError((error) {
          final errorMessage = ErrorHandler.parseErrorMessage(error);
          ErrorHandler.showErrorSnackBar(errorMessage);
        });
  }

  void onRememberYourPasswordPressed(BuildContext context) {
    Navigator.pushNamed(context, '/login');
  }

  @override
  Widget build(BuildContext context) {
    final emailController = TextEditingController();
    return Scaffold(
      appBar: AppBar(),
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
                  'Forgot Password',
                  textAlign: TextAlign.center,
                  style: TextStyle(fontSize: 20, fontWeight: FontWeight.w500),
                ),
                const SizedBox(height: AppSpacing.spacingSmall),
                Text(
                  'Enter your email to reset your password',
                  textAlign: TextAlign.center,
                  style: TextStyle(fontSize: 16),
                ),
              ],
            ),
            const SizedBox(height: AppSpacing.spacingDefault),
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                TextField(
                  controller: emailController,
                  keyboardType: TextInputType.emailAddress,
                  decoration: InputDecoration(labelText: 'Email'),
                ),
              ],
            ),
            const SizedBox(height: AppSpacing.spacingDefault),
            SizedBox(
              width: double.infinity,
              child: FilledButton(
                onPressed: () =>
                    onContinuePressed(context, emailController.text),
                child: Text('Continue'),
              ),
            ),
            const SizedBox(height: AppSpacing.spacingSmall),
            SizedBox(
              width: double.infinity,
              child: TextButton(
                onPressed: () => onRememberYourPasswordPressed(context),
                child: Text('Remember your password?'),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
