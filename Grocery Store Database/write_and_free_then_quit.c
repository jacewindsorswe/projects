/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void write_and_free_then_quit(Node **list_head_ptr, char* file_name,int *date){
	Node *prior_node = NULL, *traverse_ptr = *list_head_ptr;
	FILE *new_inv;
	new_inv = fopen(file_name,"w");
	fprintf(new_inv,"%d\t%d\n",date[0],date[1]); /*writes date to top of file */
	while(traverse_ptr != NULL){
		fprintf(new_inv, "%s\t%s\t%d\t%.2f\t%.2f\t%d\t%d\n",(traverse_ptr)->grocery_item.item,(traverse_ptr)->grocery_item.department,(traverse_ptr)->grocery_item.stock_number,(traverse_ptr)->grocery_item.pricing.retail_price,(traverse_ptr)->grocery_item.pricing.wholesale_price,(traverse_ptr)->grocery_item.pricing.retail_quantity,(traverse_ptr)->grocery_item.pricing.wholesale_quantity); /*write data inside node */
		prior_node = traverse_ptr; /*prior becomes old traverse*/
		traverse_ptr = traverse_ptr->next; /*traverse further */
		free(prior_node); /*free the old traverse */
	}
	fclose(new_inv);		
}

