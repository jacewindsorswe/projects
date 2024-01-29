/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/23/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void read_file(Node **list_head_ptr, FILE *inventory){
	int return_value = 1, items_added = 0;
	printf("Reading file to inventory...\n");
	while(return_value >= 0){
		Node *new_node = malloc(sizeof(Node));
		return_value = parse_data(inventory,&new_node);
		/*prevents any bad input from getting through */
		if(new_node->grocery_item.stock_number > 0){
			insert(list_head_ptr,new_node);
			items_added++;
		}
	}
	printf("Data retrieved successfully.\nEntered %d items into the inventory database.\n",items_added);

}

