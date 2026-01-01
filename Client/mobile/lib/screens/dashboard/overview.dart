import 'package:flutter/material.dart';
import 'package:mobile/constants/spacing.dart';
import 'package:mobile/services/storage_service.dart';

class DashboardOverviewScreen extends StatelessWidget {
  const DashboardOverviewScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      bottomNavigationBar: BottomNavigationBar(
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
          BottomNavigationBarItem(
            icon: Icon(Icons.settings),
            label: 'Settings',
          ),
        ],
      ),
      body: Center(
        child: Padding(
          padding: AppSpacing.paddingDefaultAll,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text(
                "Welcome back,",
                textAlign: TextAlign.center,
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.w500),
              ),
              FutureBuilder<String?>(
                future: StorageService.getFullName(),
                builder: (context, snapshot) {
                  final fullName = snapshot.data ?? 'Unknown User';
                  return Text(
                    "$fullName!",
                    textAlign: TextAlign.center,
                    style: TextStyle(fontSize: 24, fontWeight: FontWeight.w500),
                  );
                },
              ),
            ],
          ),
        ),
      ),
    );
  }
}
