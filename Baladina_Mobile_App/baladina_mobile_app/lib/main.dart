import 'dart:io';
import 'package:baladina_website/project_router.dart';
import 'package:flutter/material.dart';

void main() {
  HttpOverrides.global = new MyHttpOverrides();

  runApp(BaladinaRestaurant(
    router: AppRouter(),
  ));
}

class BaladinaRestaurant extends StatelessWidget {
  final AppRouter router;

  const BaladinaRestaurant({Key? key, required this.router}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      onGenerateRoute: router.generateRouter,
    );
  }
}

class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
  }
}
