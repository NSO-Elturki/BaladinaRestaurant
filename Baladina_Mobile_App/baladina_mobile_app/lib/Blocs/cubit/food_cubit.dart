import 'package:baladina_website/data/Models/food.dart';
import 'package:baladina_website/data/Repositories/food_repository.dart';
import 'package:bloc/bloc.dart';
import 'package:meta/meta.dart';
part 'food_state.dart';

class FoodCubit extends Cubit<FoodState> {
  final FoodRepository repository;
  List<Food> food = [];
  FoodCubit(this.repository) : super(FoodInitial());

  List<Food> fetchAllFood() {
    this.repository.getAll().then((food) {
      emit(LoadedFood(food));
      this.food = food;
    });

    return this.food;
  }
}
