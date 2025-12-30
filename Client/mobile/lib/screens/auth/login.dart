import 'package:flutter/material.dart';
import 'package:mobile/constants/spacing.dart';

class LoginScreen extends StatelessWidget {
  const LoginScreen({super.key});

  void onForgotPasswordPressed(BuildContext context) {
    Navigator.pushNamed(context, '/forgot-password');
  }

  void onDonHaveAnAccountPressed(BuildContext context) {
    Navigator.pushNamed(context, '/register');
  }

  @override
  Widget build(BuildContext context) {
    final emailController = TextEditingController();
    final passwordController = TextEditingController();

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
                  'Login',
                  style: TextStyle(fontSize: 20, fontWeight: FontWeight.w500),
                ),
                const SizedBox(height: AppSpacing.spacingSmall),
                Text(
                  'Enter your details to login',
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
                const SizedBox(height: AppSpacing.spacingSmall),
                TextField(
                  controller: passwordController,
                  obscureText: true,
                  decoration: InputDecoration(labelText: 'Password'),
                ),
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
                    child: Text('Continue'),
                  ),
                ),
                SizedBox(height: AppSpacing.spacingSmall),
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    SizedBox(
                      child: TextButton(
                        onPressed: () => onForgotPasswordPressed(context),
                        child: Text("Forgot password?"),
                      ),
                    ),
                    const Spacer(),
                    SizedBox(
                      child: TextButton(
                        onPressed: () => onDonHaveAnAccountPressed(context),
                        child: Text("Don't have an account?"),
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
