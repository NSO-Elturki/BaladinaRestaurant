import 'package:baladina_website/constants/strings.dart';
import 'package:flutter/cupertino.dart';

Widget getImage(int id) {
  return Container(
    width: 100,
    height: 100,
    decoration: BoxDecoration(
      image: DecorationImage(
        fit: BoxFit.fill,
        image: NetworkImage('$imageBaseUrl$id'),
      ),
    ),
  );
}
