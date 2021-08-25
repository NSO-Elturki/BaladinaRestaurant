import 'package:baladina_website/data/Models/Drink.dart';
import 'package:baladina_website/data/Models/food.dart';
import 'package:baladina_website/data/Models/order_bill.dart';
import 'package:baladina_website/data/Models/selected_item.dart';
import 'package:baladina_website/data/Repositories/order_repository.dart';
import 'package:bloc/bloc.dart';
import 'package:flutter/cupertino.dart';
import 'package:meta/meta.dart';

part 'order_state.dart';

class OrderCubit extends Cubit<OrderState> {
  final OrderRepository repository;
  OrderCubit(this.repository) : super(OrderInitial());

  int getOrderId() {
    return this.repository.orderId;
  }

  addFoodToOrder(Food food) {
    this.repository.addFoodToOrder(food);
    emit(TotalCost(this.repository.totalCost));
  }

  addDrinkToOrder(Drink drink) {
    this.repository.addDrinkToOrder(drink);
    emit(TotalCost(this.repository.totalCost));
  }

  getAllItemsOfOrder() {
    emit(LoadedOrderItems(
        this.repository.selectedItems, this.repository.totalCost));
  }

  displayTotalCost() {
    emit(TotalCost(this.repository.totalCost));
  }

  getOrderCost() {
    return this.repository.totalCost;
  }

  bool complateOrder(OrderBill orderBill) {
    this.repository.createOrderBill(orderBill).then((result) {
      if (result == true) {
        return true;
      }
    });

    return false;
  }
}
