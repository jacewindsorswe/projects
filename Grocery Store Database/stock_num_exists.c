/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

int stock_num_exists(Node **list_head_ptr, int stock_number){
	Node *traverse_ptr = *list_head_ptr;
	int exists = 1;
	while(traverse_ptr != NULL){
		/*traverse until matching number is found, then change variable to reflect that*/
		if(traverse_ptr->grocery_item.stock_number == stock_number){
			exists = 0;
			break;
		}
		traverse_ptr = traverse_ptr->next;
	}
	return exists;	
}

