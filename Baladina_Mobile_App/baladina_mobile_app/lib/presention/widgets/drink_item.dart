import 'package:baladina_website/Blocs/cubit/order_cubit.dart';
import 'package:baladina_website/data/Models/Drink.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class DrinkItem extends StatelessWidget {
  final List<Drink> drinks;

  const DrinkItem({Key? key, required this.drinks}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: drinks.length,
      itemBuilder: (context, index) {
        final item = drinks[index];
        return ListTile(
          title: Text(item.drinkName),
          trailing: Text(item.drinkPrice.toString()),
          onTap: () {
            BlocProvider.of<OrderCubit>(context).addDrinkToOrder(item)();
          },
        );
      },
    );
  }
}
