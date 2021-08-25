import 'package:baladina_website/data/Models/Drink.dart';
import 'package:baladina_website/data/Models/drinks_order.dart';
import 'package:baladina_website/data/Models/food.dart';
import 'package:baladina_website/data/Models/food_order.dart';
import 'package:baladina_website/data/Models/order_bill.dart';
import 'package:baladina_website/data/Models/selected_item.dart';
import 'package:baladina_website/data/Services/order_web_service.dart';
import 'package:baladina_website/helper_methods.dart';

class OrderRepository {
  OrderWebService orderWebService;
  double totalCost = 0;
  List<FoodOrder> foodOrderList = [];
  List<DrinksOrder> drinksOrderList = [];
  List<SelectedItem> selectedItems = [];
  late int orderId;

  OrderRepository(this.orderWebService) {
    this.orderId = HelperMethods.getRandomNumber();
    print('ORDERID IS $orderId');
  }

  addFoodToOrder(Food food) {
    this.selectedItems.add(new SelectedItem(food.foodName, food.foodPrice));
    this.foodOrderList.add(new FoodOrder(this.orderId, food.id));
    this.totalCost += food.foodPrice;
  }

  addDrinkToOrder(Drink drink) {
    this.selectedItems.add(new SelectedItem(drink.drinkName, drink.drinkPrice));
    this.drinksOrderList.add(new DrinksOrder(this.orderId, drink.id));

    this.totalCost += drink.drinkPrice;
  }

  Future<bool> createOrderBill(OrderBill orderBill) async {
    try {
      var saveItems = await this.saveOrderItems();
      if (saveItems) {
        return await this.orderWebService.addClientData(orderBill);
      }
      return false;
    } catch (e) {
      throw Exception(e.toString());
    }
  }

  Future<bool> saveOrderItems() async {
    try {
      if (this.foodOrderList.isNotEmpty && this.drinksOrderList.isEmpty) {
        return await orderWebService.addFoodOrderList(this.foodOrderList);
      }
      if (this.drinksOrderList.isNotEmpty && this.foodOrderList.isEmpty) {
        return await this
            .orderWebService
            .addDrinksOrderList(this.drinksOrderList);
      }
      if (this.foodOrderList.isNotEmpty && this.drinksOrderList.isNotEmpty) {
        var foodResponse =
            await this.orderWebService.addFoodOrderList(this.foodOrderList);
        var drinksOrderResponse =
            await this.orderWebService.addDrinksOrderList(this.drinksOrderList);

        if (foodResponse == true && drinksOrderResponse == true) {
          return true;
        } else {
          return false;
        }
      }
      if (this.foodOrderList.isEmpty && this.drinksOrderList.isEmpty) {
        return false;
      }

      return false;
    } catch (e) {
      throw Exception(e.toString());
    }
  }
}
