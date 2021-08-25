part of 'drinks_cubit.dart';

@immutable
abstract class DrinksState {}

class DrinksInitial extends DrinksState {}

class LoadedDrinks extends DrinksState {
  final List<Drink> listOfDrinks;
  LoadedDrinks(this.listOfDrinks);
}
