import 'dart:convert';
import 'package:baladina_website/constants/strings.dart';
import 'package:baladina_website/data/Models/drinks_order.dart';
import 'package:baladina_website/data/Models/food_order.dart';
import 'package:baladina_website/data/Models/order_bill.dart';
import 'package:dio/dio.dart';

class OrderWebService {
  late Dio dio;

  OrderWebService() {
    BaseOptions options = BaseOptions(
      baseUrl: baseUrl,
      receiveDataWhenStatusError: true,
      connectTimeout: 10 * 1000,
      receiveTimeout: 10 * 1000,
    );

    dio = new Dio(options);
  }

  Future<List<dynamic>> getAll() async {
    try {
      Response response = await dio.get(orders);
      return response.data;
    } catch (e) {
      print('ERROOOOOOOR ${e.toString()}');
      return [];
    }
  }

  Future<bool> addFoodOrderList(List<FoodOrder> foodOrder) async {
    try {
      String convertOrderToJson = jsonEncode(foodOrder);

      await dio.post(orderFood, data: convertOrderToJson);
      return true;
    } catch (e) {
      print('ERROOOOOOOR FROM addFoodOrderList ${e.toString()}');
      return false;
    }
  }

  Future<bool> addDrinksOrderList(List<DrinksOrder> drinksOrder) async {
    try {
      String convertOrderToJson = jsonEncode(drinksOrder);

      await dio.post(orderDrink, data: convertOrderToJson);
      return true;
    } catch (e) {
      print('ERROOOOOOOR FROM addDrinksOrderList ${e.toString()}');
      return false;
    }
  }

  Future<bool> addClientData(OrderBill orderBill) async {
    try {
      String convertDataToJson = jsonEncode(orderBill);
      await dio.post(orders, data: convertDataToJson);
      return true;
    } catch (e) {
      print('ERROOOOOOOR FROM addClientData ${e.toString()}');
      return false;
    }
  }
}
