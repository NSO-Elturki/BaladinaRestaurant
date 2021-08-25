import 'package:baladina_website/data/Models/Drink.dart';
import 'package:baladina_website/data/Services/drink_web_service.dart';

class DrinkRepository {
  DrinkWebService drinkWebService;

  DrinkRepository(this.drinkWebService);

  Future<List<Drink>> getAll() async {
    final drinks = await this.drinkWebService.getAll();
    return drinks.map((drink) => Drink.fromJson(drink)).toList();
  }
}
