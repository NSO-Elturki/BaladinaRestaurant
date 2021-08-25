import 'package:baladina_website/Blocs/cubit/drinks_cubit.dart';
import 'package:baladina_website/Blocs/cubit/food_cubit.dart';
import 'package:baladina_website/Blocs/cubit/order_cubit.dart';
import 'package:baladina_website/constants/strings.dart';
import 'package:baladina_website/data/Models/Drink.dart';
import 'package:baladina_website/data/Models/food.dart';
import 'package:baladina_website/presention/widgets/drink_item.dart';
import 'package:baladina_website/presention/widgets/food_item.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  late List<Food> listOfFood;
  late List<Drink> listOfDrinks;
  late int totalItems;
  late double totalCost;

  @override
  void initState() {
    super.initState();

    BlocProvider.of<FoodCubit>(context).fetchAllFood();
    BlocProvider.of<DrinksCubit>(context).fetchAllDrinks();
    BlocProvider.of<OrderCubit>(context).displayTotalCost();
  }

  Widget foodList(String type) {
    return BlocBuilder<FoodCubit, FoodState>(
      builder: (context, state) {
        if (state is LoadedFood) {
          this.listOfFood = (state).listOfFood;
          return FoodItem(
            food: this.listOfFood,
            type: type,
          );
        } else {
          return Center(child: Text('no food!'));
        }
      },
    );
  }

  Widget drinksList() {
    return BlocBuilder<DrinksCubit, DrinksState>(
      builder: (context, state) {
        if (state is LoadedDrinks) {
          this.listOfDrinks = (state).listOfDrinks;
          return DrinkItem(drinks: listOfDrinks);
        } else {
          return Center(child: Text('no Drinks!'));
        }
      },
    );
  }

  Widget totalCostOfSelectedItems() {
    return BlocBuilder<OrderCubit, OrderState>(
      builder: (context, state) {
        if (state is TotalCost) {
          this.totalCost = state.total;
          return Text('(${totalCost.toString()})');
        } else {
          return Text('(${totalCost.toString()})');
        }
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 4,
      child: Scaffold(
        appBar: AppBar(
          title: Text("Baladina restaurant"),
          backgroundColor: Colors.blue[900],
          bottom: TabBar(
            tabs: [
              Tab(
                text: 'Starters',
              ),
              Tab(
                text: 'Main dishs',
              ),
              Tab(
                text: 'Deserts',
              ),
              Tab(
                text: 'Drinks',
              ),
            ],
          ),
        ),
        body: TabBarView(
          children: [
            foodList('Starter'),
            foodList('Main dish'),
            foodList('Dessert'),
            drinksList(),
          ],
        ),
        bottomSheet: Container(
          width: MediaQuery.of(context).size.width,
          padding: EdgeInsets.only(right: 10, left: 10),
          child: ElevatedButton(
            style: ElevatedButton.styleFrom(primary: Colors.blue[900]),
            onPressed: () {
              Navigator.pushNamed(context, basketPage, arguments: context);
            },
            child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Text('Basket'),
                SizedBox(
                  width: 2,
                ),
                totalCostOfSelectedItems()
              ],
            ),
          ),
        ),
      ),
    );
  }
}
