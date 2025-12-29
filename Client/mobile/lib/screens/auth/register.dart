import 'package:flutter/material.dart';
import 'package:mobile/constants/spacing.dart';

class RegisterPage extends StatelessWidget {
  const RegisterPage({super.key});

  void onAlreadyHaveAnAccountPressed(BuildContext context) {
    Navigator.pushNamed(context, '/login');
  }

  void onForgotPasswordPressed(BuildContext context) {
    // TODO: Implement forgot password
  }

  @override
  Widget build(BuildContext context) {
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
                  'Create an account',
                  style: TextStyle(fontSize: 20, fontWeight: FontWeight.w500),
                ),
                const SizedBox(height: AppSpacing.spacingSmall),
                Text(
                  'Enter your details to create an account',
                  style: TextStyle(fontSize: 16),
                ),
              ],
            ),
            const SizedBox(height: AppSpacing.spacingDefault),
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                TextField(decoration: InputDecoration(labelText: 'Full Name')),
              ],
            ),
            const SizedBox(height: AppSpacing.spacingDefault),
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                TextField(decoration: InputDecoration(labelText: 'Email')),
              ],
            ),
            const SizedBox(height: AppSpacing.spacingDefault),
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                TextField(decoration: InputDecoration(labelText: 'Password')),
              ],
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
                    child: Text('Create account'),
                  ),
                ),
                SizedBox(height: AppSpacing.spacingSmall),
                SizedBox(
                  width: double.infinity,
                  child: TextButton(
                    onPressed: () => onAlreadyHaveAnAccountPressed(context),
                    child: Text("Already have an account?"),
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
