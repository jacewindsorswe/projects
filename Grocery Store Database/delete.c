/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void delete(Node **list_head_ptr, char* dont_use, int* date_dont_use){
	int stock_number;
	printf("Enter the stock number of the item you'd like to delete: ");
	scanf(" %d", &stock_number);
	delete_node(list_head_ptr,stock_number);
}

