/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void add_item(Node **list_head_ptr,char* dont_use,int *date_dont_use){
	Node *new_node = malloc(sizeof(Node));
	printf("Enter Grocery Item Name: ");
	scanf(" %[^\n]",(char *)new_node->grocery_item.item);
	printf("Enter Department: ");
	scanf(" %[^\n]",(char*)new_node->grocery_item.department);
	printf("Enter Stock Number: ");
	scanf(" %d",&new_node->grocery_item.stock_number);
	printf("Enter Retail Price: ");
	scanf(" %f",&new_node->grocery_item.pricing.retail_price);
	printf("Enter Wholesale Price: ");
	scanf(" %f",&new_node->grocery_item.pricing.wholesale_price);
	printf("Enter Retail Quantity: ");
	scanf(" %d",&new_node->grocery_item.pricing.retail_quantity);
	printf("Enter Wholesale Quantity: ");
	scanf(" %d",&new_node->grocery_item.pricing.wholesale_quantity);
	/*adds the item if the stock number doesn't already exist, otherwise reports error */
	if(stock_num_exists(list_head_ptr,new_node->grocery_item.stock_number) > 0){
		insert(list_head_ptr,new_node);	
	} else{
		printf("Error: Stock number already present in items.\n");
	}
}

