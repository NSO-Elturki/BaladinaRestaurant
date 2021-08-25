import 'package:baladina_website/Blocs/cubit/order_cubit.dart';
import 'package:baladina_website/data/Models/food.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'get_image.dart';

class FoodItem extends StatelessWidget {
  final List<Food> food;
  final String type;

  const FoodItem({Key? key, required this.food, required this.type})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return foodFactoryList(this.type, this.food, context);
  }

  Widget foodFactoryList(
      String type, List<Food> foodList, BuildContext context) {
    var tempList = [];
    if (type == 'Starter') {
      for (Food i in foodList) {
        if (i.foodType == type) {
          tempList.add(i);
        }
      }
    }

    if (type == 'Main dish') {
      for (Food i in foodList) {
        if (i.foodType == type) {
          tempList.add(i);
        }
      }
    }

    if (type == 'Dessert') {
      for (Food i in foodList) {
        if (i.foodType == type) {
          tempList.add(i);
        }
      }
    }

    return ListView.builder(
      itemCount: tempList.length,
      itemBuilder: (context, index) {
        final item = tempList[index];
        return ListTile(
          title: Text(item.foodName),
          subtitle: Text(item.foodDescription),
          leading: getImage(item.id),
          trailing: Text('${item.foodPrice.toString()}'),
          onTap: () {
            Food food = item as Food;
            BlocProvider.of<OrderCubit>(context).addFoodToOrder(food)();
            print(food.foodName);
          },
        );
      },
    );
  }
}
