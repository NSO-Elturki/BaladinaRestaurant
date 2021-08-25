// import 'package:baladina_website/Blocs/cubit/order_cubit.dart';
// import 'package:baladina_website/data/Models/order_bill.dart';
// import 'package:flutter/material.dart';
// import 'package:flutter_bloc/flutter_bloc.dart';
// import 'package:font_awesome_flutter/font_awesome_flutter.dart';

// class CompleteOrderPage extends StatefulWidget {
//   final double orderCost;
//   const CompleteOrderPage({Key? key, required this.orderCost})
//       : super(key: key);

//   @override
//   CompleteOrderPageState createState() {
//     return CompleteOrderPageState();
//   }
// }

// class CompleteOrderPageState extends State<CompleteOrderPage> {
//   final _formKey = GlobalKey<FormState>();
//   late int phoneNumber, orderId;
//   late String name, address, email, note, city;

//   @override
//   Widget build(BuildContext context) {
//     this.orderId = BlocProvider.of<OrderCubit>(context).getOrderId();

//     return Scaffold(
//       body: Container(
//         padding: EdgeInsets.all(10),
//         child: Form(
//           key: _formKey,
//           child: Column(
//             crossAxisAlignment: CrossAxisAlignment.start,
//             children: [
//               Expanded(
//                 child: Row(
//                   children: [
//                     FaIcon(FontAwesomeIcons.user),
//                     SizedBox(
//                       width: 2,
//                     ),
//                     Expanded(
//                       child: Container(
//                         decoration: BoxDecoration(
//                           border: Border.all(
//                             color: Colors.black, // red as border color
//                           ),
//                         ),
//                         child: TextFormField(
//                           decoration: InputDecoration(hintText: 'Full name'),
//                           validator: (value) {
//                             if (value == null || value.isEmpty) {
//                               return 'Please enter some text';
//                             }
//                             this.name = value;
//                           },
//                         ),
//                       ),
//                     ),
//                   ],
//                 ),
//               ),
//               SizedBox(
//                 height: 3,
//               ),
//               Expanded(
//                 child: Row(
//                   children: [
//                     FaIcon(FontAwesomeIcons.mapMarked),
//                     SizedBox(
//                       width: 2,
//                     ),
//                     Expanded(
//                       child: Container(
//                         decoration: BoxDecoration(
//                           border: Border.all(
//                             color: Colors.black, // red as border color
//                           ),
//                         ),
//                         child: TextFormField(
//                           decoration: InputDecoration(hintText: 'Address'),
//                           validator: (value) {
//                             if (value == null || value.isEmpty) {
//                               return 'Please enter some text';
//                             }
//                             this.address = value;
//                           },
//                         ),
//                       ),
//                     ),
//                   ],
//                 ),
//               ),
//               SizedBox(
//                 height: 3,
//               ),
//               Expanded(
//                 child: Row(
//                   children: [
//                     FaIcon(FontAwesomeIcons.at),
//                     SizedBox(
//                       width: 2,
//                     ),
//                     Expanded(
//                       child: Container(
//                         decoration: BoxDecoration(
//                           border: Border.all(
//                             color: Colors.black, // red as border color
//                           ),
//                         ),
//                         child: TextFormField(
//                           decoration: InputDecoration(hintText: 'Email'),
//                           validator: (value) {
//                             if (value == null || value.isEmpty) {
//                               return 'Please enter some text';
//                             }
//                             this.email = value;
//                           },
//                         ),
//                       ),
//                     ),
//                   ],
//                 ),
//               ),
//               SizedBox(
//                 height: 3,
//               ),
//               Expanded(
//                 child: Row(
//                   children: [
//                     FaIcon(FontAwesomeIcons.phone),
//                     SizedBox(
//                       width: 2,
//                     ),
//                     Expanded(
//                       child: Container(
//                         decoration: BoxDecoration(
//                           border: Border.all(
//                             color: Colors.black,
//                           ),
//                         ),
//                         child: TextFormField(
//                           decoration: InputDecoration(hintText: 'Phone number'),
//                           validator: (value) {
//                             if (value == null || value.isEmpty) {
//                               return 'Please enter some text';
//                             }
//                             this.phoneNumber = int.parse(value);
//                           },
//                         ),
//                       ),
//                     ),
//                   ],
//                 ),
//               ),
//               SizedBox(
//                 height: 3,
//               ),
//               Expanded(
//                 child: Row(
//                   children: [
//                     FaIcon(FontAwesomeIcons.city),
//                     SizedBox(
//                       width: 2,
//                     ),
//                     Expanded(
//                       child: Container(
//                         decoration: BoxDecoration(
//                           border: Border.all(
//                             color: Colors.black,
//                           ),
//                         ),
//                         child: TextFormField(
//                           decoration: InputDecoration(hintText: 'City'),
//                           validator: (value) {
//                             if (value == null || value.isEmpty) {
//                               return 'Please enter some text';
//                             }
//                             this.city = value;
//                           },
//                         ),
//                       ),
//                     ),
//                   ],
//                 ),
//               ),
//               SizedBox(
//                 height: 3,
//               ),
//               Expanded(
//                 child: Row(
//                   children: [
//                     FaIcon(FontAwesomeIcons.stickyNote),
//                     SizedBox(
//                       width: 2,
//                     ),
//                     Expanded(
//                       child: Container(
//                         decoration: BoxDecoration(
//                           border: Border.all(
//                             color: Colors.black,
//                           ),
//                         ),
//                         child: TextFormField(
//                           decoration: InputDecoration(hintText: 'Note'),
//                           validator: (value) {
//                             if (value == null || value.isEmpty) {
//                               return 'Please enter some text';
//                             }
//                             this.note = value;
//                           },
//                         ),
//                       ),
//                     ),
//                   ],
//                 ),
//               ),
//               Expanded(
//                 child: Container(
//                   width: MediaQuery.of(context).size.width,
//                   padding: EdgeInsets.only(right: 10, left: 10),

//                   //  padding: const EdgeInsets.symmetric(vertical: 16.0),
//                   child: ElevatedButton(
//                     style: ElevatedButton.styleFrom(primary: Colors.blue[900]),
//                     onPressed: () {
//                       if (_formKey.currentState!.validate()) {
//                         BlocProvider.of<OrderCubit>(context).complateOrder(
//                             new OrderBill(
//                                 this.orderId,
//                                 this.name,
//                                 this.phoneNumber,
//                                 this.address,
//                                 this.email,
//                                 this.city,
//                                 this.note,
//                                 widget.orderCost));
//                       } else {
//                         ScaffoldMessenger.of(context).showSnackBar(
//                           const SnackBar(content: Text('Processing Data')),
//                         );
//                       }
//                     },
//                     child: const Text('Submit'),
//                   ),
//                 ),
//               ),
//             ],
//           ),
//         ),
//       ),
//     );
//   }
// }

import 'package:baladina_website/Blocs/cubit/order_cubit.dart';
import 'package:baladina_website/data/Models/order_bill.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

class CompleteOrderPage extends StatefulWidget {
  final double orderCost;
  const CompleteOrderPage({Key? key, required this.orderCost})
      : super(key: key);

  @override
  CompleteOrderPageState createState() {
    return CompleteOrderPageState();
  }
}

class CompleteOrderPageState extends State<CompleteOrderPage> {
  final _formKey = GlobalKey<FormState>();
  late int phoneNumber, orderId;
  late String name, address, email, note, city;

  @override
  Widget build(BuildContext context) {
    this.orderId = BlocProvider.of<OrderCubit>(context).getOrderId();

    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.blue[900],
        title: Text('Complete order'),
      ),
      body: GestureDetector(
        onTap: () {
          FocusScope.of(context).requestFocus(new FocusNode());
        },
        child: SingleChildScrollView(
          child: Container(
            padding: EdgeInsets.all(40),
            child: Form(
              key: _formKey,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    children: [
                      FaIcon(FontAwesomeIcons.user),
                      SizedBox(
                        width: 10,
                      ),
                      Expanded(
                        child: Container(
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.all(Radius.circular(10)),
                            border: Border.all(
                              color: Colors.black, // red as border color
                            ),
                          ),
                          child: TextFormField(
                            decoration: InputDecoration(hintText: 'Full name'),
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Please enter your full name';
                              }
                              this.name = value;
                            },
                          ),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(
                    height: 30,
                  ),
                  Row(
                    children: [
                      FaIcon(FontAwesomeIcons.mapMarked),
                      SizedBox(
                        width: 10,
                      ),
                      Expanded(
                        child: Container(
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.all(Radius.circular(10)),
                            border: Border.all(
                              color: Colors.black, // red as border color
                            ),
                          ),
                          child: TextFormField(
                            decoration: InputDecoration(hintText: 'Address'),
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Please enter address';
                              }
                              this.address = value;
                            },
                          ),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(
                    height: 30,
                  ),
                  Row(
                    children: [
                      FaIcon(FontAwesomeIcons.at),
                      SizedBox(
                        width: 10,
                      ),
                      Expanded(
                        child: Container(
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.all(Radius.circular(10)),
                            border: Border.all(
                              color: Colors.black, // red as border color
                            ),
                          ),
                          child: TextFormField(
                            decoration: InputDecoration(hintText: 'Email'),
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Please enter email';
                              }
                              this.email = value;
                            },
                          ),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(
                    height: 30,
                  ),
                  Row(
                    children: [
                      FaIcon(FontAwesomeIcons.phone),
                      SizedBox(
                        width: 10,
                      ),
                      Expanded(
                        child: Container(
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.all(Radius.circular(10)),
                            border: Border.all(
                              color: Colors.black,
                            ),
                          ),
                          child: TextFormField(
                            decoration:
                                InputDecoration(hintText: 'Phone number'),
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Please enter phone number';
                              }
                              this.phoneNumber = int.parse(value);
                            },
                          ),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(
                    height: 30,
                  ),
                  Row(
                    children: [
                      FaIcon(FontAwesomeIcons.city),
                      SizedBox(
                        width: 10,
                      ),
                      Expanded(
                        child: Container(
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.all(Radius.circular(10)),
                            border: Border.all(
                              color: Colors.black,
                            ),
                          ),
                          child: TextFormField(
                            decoration: InputDecoration(hintText: 'City'),
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Please enter your city';
                              }
                              this.city = value;
                            },
                          ),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(
                    height: 30,
                  ),
                  Row(
                    children: [
                      FaIcon(FontAwesomeIcons.stickyNote),
                      SizedBox(
                        width: 10,
                      ),
                      Expanded(
                        child: Container(
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.all(Radius.circular(10)),
                            border: Border.all(
                              color: Colors.black,
                            ),
                          ),
                          child: TextFormField(
                            keyboardType: TextInputType.multiline,
                            maxLines: 3,
                            decoration: InputDecoration(hintText: 'Note'),
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                value = 'Null';
                              }
                              this.note = value;
                            },
                          ),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(
                    height: 45,
                  ),
                  Container(
                    width: MediaQuery.of(context).size.width,
                    padding: EdgeInsets.only(right: 10, left: 10),
                    child: ElevatedButton(
                      style:
                          ElevatedButton.styleFrom(primary: Colors.blue[900]),
                      onPressed: () {
                        if (_formKey.currentState!.validate()) {
                          BlocProvider.of<OrderCubit>(context).complateOrder(
                              new OrderBill(
                                  this.orderId,
                                  this.name,
                                  this.phoneNumber,
                                  this.address,
                                  this.email,
                                  this.city,
                                  this.note,
                                  widget.orderCost));
                        } else {
                          ScaffoldMessenger.of(context).showSnackBar(
                            const SnackBar(content: Text('Processing Data')),
                          );
                        }
                      },
                      child: const Text('Submit'),
                    ),
                  )
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
