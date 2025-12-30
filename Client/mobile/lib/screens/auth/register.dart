import 'package:flutter/material.dart';
import 'package:mobile/constants/spacing.dart';
import 'package:mobile/main.dart';
import 'package:mobile/services/auth_service.dart';

class RegisterPage extends StatelessWidget {
  const RegisterPage({super.key});

  void onAlreadyHaveAnAccountPressed(BuildContext context) {
    Navigator.pushNamed(context, '/login');
  }

  void onRegisterPressed(
    BuildContext context,
    String fullName,
    String email,
    String password,
  ) {
    final nameParts = fullName.trim().split(' ');
    final firstName = nameParts.isNotEmpty ? nameParts[0] : '';
    final lastName = nameParts.length > 1 ? nameParts.sublist(1).join(' ') : '';

    AuthService.register(firstName, lastName, email, password)
        .then((_) {
          Navigator.pushNamed(context, '/login');
          scaffoldMessengerKey.currentState?.showSnackBar(
            const SnackBar(
              content: Text('Please check your email for verification'),
              duration: Duration(seconds: 5),
            ),
          );
        })
        .catchError((error) {
          final errorMessage = error is Exception
              ? error.toString().replaceFirst('Exception: ', '')
              : error.toString();
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
    final fullNameController = TextEditingController();
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
            TextField(
              controller: fullNameController,
              decoration: InputDecoration(labelText: 'Full Name'),
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
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
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
                    onPressed: () => onRegisterPressed(
                      context,
                      fullNameController.text,
                      emailController.text,
                      passwordController.text,
                    ),
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
