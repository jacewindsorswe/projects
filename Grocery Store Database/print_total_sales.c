/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void print_total_sales(Node *list_head_ptr){
	Node *traverse_ptr = list_head_ptr;
	int sales = 0;
	/*sums the number of products sold */
	while(traverse_ptr != NULL){
		sales += traverse_ptr->grocery_item.pricing.retail_quantity;
		traverse_ptr = traverse_ptr->next;
	}
	printf("Total sales: %d items\n",sales);	
}

