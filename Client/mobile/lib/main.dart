import 'package:flutter/material.dart';
import 'package:mobile/screens/auth/forgot_password.dart';
import 'package:mobile/screens/auth/login.dart';
import 'package:mobile/screens/auth/register.dart';
import 'package:mobile/screens/auth/verification_notice.dart';
import 'package:mobile/screens/dashboard/overview.dart';
import 'package:mobile/screens/onboarding.dart';
import 'package:mobile/services/api_service.dart';
import 'package:mobile/widgets/common/auth_guard.dart';
import 'package:mobile/widgets/common/guest_guard.dart';

final GlobalKey<ScaffoldMessengerState> scaffoldMessengerKey =
    GlobalKey<ScaffoldMessengerState>();
final GlobalKey<NavigatorState> navigatorKey = GlobalKey<NavigatorState>();

void main() {
  ApiService.Initialize();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      navigatorKey: navigatorKey,
      scaffoldMessengerKey: scaffoldMessengerKey,
      initialRoute: '/',
      routes: {
        '/': (context) => GuestGuardWidget(child: const OnboardingScreen()),
        '/login': (context) => GuestGuardWidget(child: const LoginScreen()),
        '/register': (context) => GuestGuardWidget(child: const RegisterPage()),
        '/verification-notice': (context) =>
            GuestGuardWidget(child: const VerificationNoticeScreen()),
        '/forgot-password': (context) =>
            GuestGuardWidget(child: const ForgotPasswordScreen()),
        '/dashboard': (context) =>
            AuthGuardWidget(child: const DashboardOverviewScreen()),
      },
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.green),
      ),
      debugShowCheckedModeBanner: false,
    );
  }
}
