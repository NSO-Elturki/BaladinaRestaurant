part of 'order_cubit.dart';

@immutable
abstract class OrderState {}

class OrderInitial extends OrderState {}

class LoadedOrderItems extends OrderState {
  final List<SelectedItem> selectedItems;
  final double totalCost;

  LoadedOrderItems(this.selectedItems, this.totalCost);
}

class TotalCost extends OrderState {
  final double total;
  TotalCost(this.total);
}
