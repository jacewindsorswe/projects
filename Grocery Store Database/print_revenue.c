/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void print_revenue(Node *list_head_ptr){
	Node *traverse_ptr = list_head_ptr;
	float total = 0;
	/*prints current revenue retail quantity */
	while(traverse_ptr!=NULL){
		total += (traverse_ptr->grocery_item.pricing.retail_quantity * traverse_ptr->grocery_item.pricing.retail_price);
	traverse_ptr = traverse_ptr->next;
	}
	printf("Total revenue: $%.2f\n",total);
}

