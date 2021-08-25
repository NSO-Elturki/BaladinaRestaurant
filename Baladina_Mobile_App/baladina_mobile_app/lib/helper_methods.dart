import 'dart:math';

class HelperMethods {
  static int getRandomNumber() {
    return 2 + Random().nextInt(1000 - 2);
  }
}
