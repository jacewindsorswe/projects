/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void print_items_in_stock(Node *list_head_ptr){
	Node *traverse_ptr = list_head_ptr;
	printf("Grocery items in stock:\n");
	printf("%-15s%-15s%-20s%-20s\n","Stock #","Quantity","Department","Item");	
	while(traverse_ptr!=NULL){
		if(traverse_ptr->grocery_item.pricing.wholesale_quantity - traverse_ptr->grocery_item.pricing.retail_quantity > 0){
		print_item_info(traverse_ptr);
		}
		traverse_ptr = traverse_ptr->next;
	}
}

