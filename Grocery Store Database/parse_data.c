/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/15/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

int parse_data(FILE *inventory, Node **new_node_ptr){
	int returnVal;
	returnVal = fscanf(inventory, " %[^\t] %[^\t] %d %f %f %d %d",(char*)(*new_node_ptr)->grocery_item.item,(char*)(*new_node_ptr)->grocery_item.department,&(*new_node_ptr)->grocery_item.stock_number,&(*new_node_ptr)->grocery_item.pricing.retail_price,&(*new_node_ptr)->grocery_item.pricing.wholesale_price,&(*new_node_ptr)->grocery_item.pricing.retail_quantity,&(*new_node_ptr)->grocery_item.pricing.wholesale_quantity);
	(*new_node_ptr)->next = NULL;
	return returnVal;
} 
