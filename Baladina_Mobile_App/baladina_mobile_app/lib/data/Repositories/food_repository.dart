import 'package:baladina_website/data/Models/food.dart';
import 'package:baladina_website/data/Services/food_web_service.dart';

class FoodRepository {
  FoodWebService foodWebService;

  FoodRepository(this.foodWebService);

  Future<List<Food>> getAll() async {
    final food = await this.foodWebService.getAll();
    return food.map((food) => Food.fromJson(food)).toList();
  }
}
