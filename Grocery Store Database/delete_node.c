/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void delete_node(Node **list_head_ptr, int stock_number){
	Node *prior_node = NULL, *traverse_ptr = *list_head_ptr;
	while(traverse_ptr!=NULL && traverse_ptr->grocery_item.stock_number!=stock_number){
		prior_node = traverse_ptr;
		traverse_ptr = traverse_ptr->next;
	
	}/*handles empty list */
	if(traverse_ptr == NULL && prior_node == NULL){
		printf("Error: List is already empty.\n");
	/*handles item not found */
	}else if(traverse_ptr == NULL){ 
		printf("Error: item not found.\n");
	/*handles front */
	}else if(prior_node == NULL){
		*list_head_ptr = traverse_ptr->next;
		free(traverse_ptr);
		printf("Item successfully removed.\n");
	} else{ /*handles non-exception */
		prior_node->next = traverse_ptr->next;
		free(traverse_ptr);
		printf("Item successfully removed.\n");
	}
}
