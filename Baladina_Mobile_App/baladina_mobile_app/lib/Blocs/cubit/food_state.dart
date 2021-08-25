part of 'food_cubit.dart';

@immutable
abstract class FoodState {}

class FoodInitial extends FoodState {}

class LoadedFood extends FoodState {
  final List<Food> listOfFood;
  LoadedFood(this.listOfFood);
}
