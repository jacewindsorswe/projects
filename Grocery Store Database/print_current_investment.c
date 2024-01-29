/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void print_current_investment(Node *list_head_ptr){
	Node *traverse_ptr = list_head_ptr;
	float total = 0;
	/*sums the total investment (wholesale price * wholesale quantity) */
	while(traverse_ptr!=NULL){
		total += traverse_ptr->grocery_item.pricing.wholesale_price * (traverse_ptr->grocery_item.pricing.wholesale_quantity - traverse_ptr->grocery_item.pricing.retail_quantity);
	traverse_ptr = traverse_ptr->next;
	}
	printf("Current investment in product: $%.2f\n",total);
}

