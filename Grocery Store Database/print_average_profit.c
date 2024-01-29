/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void print_average_profit(Node *list_head_ptr){
	Node *traverse_ptr = list_head_ptr;
	float total = 0,revenue = 0,wholesale_cost = 0, cost_inv = 0;
	int sales = 0;
	/*collects values needed for avg profit - profit and num sales */
	while(traverse_ptr!=NULL){
		revenue += (traverse_ptr->grocery_item.pricing.retail_quantity * traverse_ptr->grocery_item.pricing.retail_price);
		wholesale_cost += (traverse_ptr->grocery_item.pricing.wholesale_quantity * traverse_ptr->grocery_item.pricing.wholesale_price);
		cost_inv += traverse_ptr->grocery_item.pricing.wholesale_price * (traverse_ptr->grocery_item.pricing.wholesale_quantity - traverse_ptr->grocery_item.pricing.retail_quantity); 
		sales += traverse_ptr->grocery_item.pricing.retail_quantity;
		traverse_ptr = traverse_ptr->next;
	}
	total = revenue - wholesale_cost + cost_inv;
	total = total / sales;
	printf("Average profit per item: $%.2f\n",total);
}

