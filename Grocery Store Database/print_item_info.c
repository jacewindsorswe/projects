/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/25/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void print_item_info(Node *item){
		printf("%-15d%-15d%-20s%-20s\n", item->grocery_item.stock_number, (item->grocery_item.pricing.wholesale_quantity - item->grocery_item.pricing.retail_quantity), item->grocery_item.department, item->grocery_item.item);
}

