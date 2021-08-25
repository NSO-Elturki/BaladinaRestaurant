import 'package:baladina_website/Blocs/cubit/drinks_cubit.dart';
import 'package:baladina_website/Blocs/cubit/food_cubit.dart';
import 'package:baladina_website/Blocs/cubit/order_cubit.dart';
import 'package:baladina_website/data/Repositories/drink_repository.dart';
import 'package:baladina_website/data/Repositories/food_repository.dart';
import 'package:baladina_website/data/Repositories/order_repository.dart';
import 'package:baladina_website/data/Services/drink_web_service.dart';
import 'package:baladina_website/data/Services/food_web_service.dart';
import 'package:baladina_website/data/Services/order_web_service.dart';
import 'package:baladina_website/presention/pages/basket_page.dart';
import 'package:baladina_website/presention/pages/complete_order_page.dart';
import 'package:flutter/material.dart';
import 'package:baladina_website/presention/pages/home_page.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'constants/strings.dart';

class AppRouter {
  late FoodRepository foodRepository;
  late FoodCubit foodCubit;

  late DrinkRepository drinkRepository;
  late DrinksCubit drinksCubit;

  late OrderRepository orderRepository;
  late OrderCubit ordersCubit;

  AppRouter() {
    this.foodRepository = FoodRepository(FoodWebService());
    this.foodCubit = FoodCubit(this.foodRepository);

    this.drinkRepository = DrinkRepository(DrinkWebService());
    this.drinksCubit = DrinksCubit(this.drinkRepository);

    this.orderRepository = OrderRepository(OrderWebService());
    this.ordersCubit = OrderCubit(this.orderRepository);
  }

  Route? generateRouter(RouteSettings settings) {
    switch (settings.name) {
      case homePage:
        return MaterialPageRoute(
            builder: (_) => MultiBlocProvider(
                  providers: [
                    BlocProvider<FoodCubit>(
                      create: (BuildContext context) => foodCubit,
                    ),
                    BlocProvider<DrinksCubit>(
                      create: (BuildContext context) => drinksCubit,
                    ),
                    BlocProvider<OrderCubit>(
                      create: (BuildContext context) => ordersCubit,
                    ),
                  ],
                  child: HomePage(),
                ));

      case basketPage:
        final context = settings.arguments as BuildContext;
        return MaterialPageRoute(
            builder: (_) => BlocProvider.value(
                  value: BlocProvider.of<OrderCubit>(context),
                  child: BasketPage(),
                ));

      case completeOrderPage:
        final args = settings.arguments as List;
        return MaterialPageRoute(
            builder: (_) => BlocProvider.value(
                  value: BlocProvider.of<OrderCubit>(args[0]),
                  child: CompleteOrderPage(
                    orderCost: args[1],
                  ),
                ));
    }
  }
}
