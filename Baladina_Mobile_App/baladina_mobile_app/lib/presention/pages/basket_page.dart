import 'package:baladina_website/Blocs/cubit/order_cubit.dart';
import 'package:baladina_website/constants/strings.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class BasketPage extends StatefulWidget {
  @override
  _BasketPageState createState() => _BasketPageState();
}

class _BasketPageState extends State<BasketPage> {
  Widget allSelectedItemsOfOrder() {
    return BlocBuilder<OrderCubit, OrderState>(
      builder: (context, state) {
        if (state is LoadedOrderItems) {
          if (state.selectedItems.isNotEmpty) {
            return Column(
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                Container(
                  height: MediaQuery.of(context).size.height / 2,
                  child: ListView.separated(
                    separatorBuilder: (context, index) => Divider(
                      color: Colors.black,
                    ),
                    itemCount: state.selectedItems.length,
                    itemBuilder: (context, index) {
                      final item = state.selectedItems[index];
                      return Column(
                        children: [
                          ListTile(
                            leading: Text(item.quantity.toString()),
                            title: Text(item.name),
                            trailing: Text('${item.price.toString()}'),
                          ),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [],
                          )
                        ],
                      );
                    },
                  ),
                ),
                Expanded(
                    child: Container(
                  padding: EdgeInsets.only(left: 15, right: 15),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [Text('Total:'), Text('${state.totalCost}')],
                  ),
                ))
              ],
            );
          } else {
            return Center(child: Text('No selected item!'));
          }
        } else {
          return Text('');
        }
      },
    );
  }

  @override
  void initState() {
    super.initState();

    BlocProvider.of<OrderCubit>(context).getAllItemsOfOrder();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.blue[900],
        title: Text('Basket'),
      ),
      body: allSelectedItemsOfOrder(),
      bottomSheet: Container(
        width: MediaQuery.of(context).size.width,
        padding: EdgeInsets.only(right: 10, left: 10),
        child: ElevatedButton(
          style: ElevatedButton.styleFrom(primary: Colors.blue[900]),
          onPressed: () {
            var orderCost = BlocProvider.of<OrderCubit>(context).getOrderCost();
            Navigator.pushNamed(context, completeOrderPage,
                arguments: [context, orderCost]);
          },
          child: Row(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text('Order'),
              SizedBox(
                width: 2,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
