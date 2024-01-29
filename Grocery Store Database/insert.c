/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/15/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void insert(Node **list_head_ptr,Node *new_node_ptr){
	Node *traverse_ptr = *list_head_ptr, *prior_node = NULL;
	while(traverse_ptr != NULL && new_node_ptr->grocery_item.stock_number < traverse_ptr->grocery_item.stock_number){ 
	prior_node = traverse_ptr;
	traverse_ptr = traverse_ptr->next;
	} /*handles empty list */
	if(traverse_ptr == NULL && prior_node == NULL){
		*list_head_ptr = new_node_ptr;
		new_node_ptr->next = NULL;
	/*handles insert at front */
	}else if(prior_node == NULL){
		new_node_ptr->next = *list_head_ptr;
		*list_head_ptr = new_node_ptr;
	}else{ /*handles non-exception */
		new_node_ptr->next = traverse_ptr;
		prior_node->next = new_node_ptr;
	}	

}

