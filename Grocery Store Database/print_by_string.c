/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */

#define _GNU_SOURCE
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "lab4.h"

void print_by_string(Node *list_head_ptr){
	Node *traverse_ptr = list_head_ptr;
	const char substr[256]; /*Dept name can't be larger than 30 */
	printf("Enter Department Name to print: ");
	scanf(" %[^\n]",&substr);
	printf("Printing items from department %s:\n",&substr); 
	printf("%-15s%-15s%-20s%-20s\n","Stock #","Quantity","Department","Item");	
	while(traverse_ptr!=NULL){
		/*prints item if found to be substring of that item's department */
		if(strcasestr(traverse_ptr->grocery_item.department,substr)){
		print_item_info(traverse_ptr);
		}
		traverse_ptr = traverse_ptr->next;
	}
}

