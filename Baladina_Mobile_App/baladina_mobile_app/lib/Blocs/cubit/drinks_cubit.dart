import 'package:baladina_website/data/Models/Drink.dart';
import 'package:baladina_website/data/Repositories/drink_repository.dart';
import 'package:bloc/bloc.dart';
import 'package:meta/meta.dart';
part 'drinks_state.dart';

class DrinksCubit extends Cubit<DrinksState> {
  final DrinkRepository repository;
  List<Drink> drinks = [];
  DrinksCubit(this.repository) : super(DrinksInitial());

  List<Drink> fetchAllDrinks() {
    this.repository.getAll().then((drinks) {
      emit(LoadedDrinks(drinks));
      this.drinks = drinks;
    });

    return this.drinks;
  }
}
