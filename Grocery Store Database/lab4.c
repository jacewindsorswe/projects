/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/15/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

int main(int argc, char** argv){
	int *option,date[2],edit_or_not = 0;
	void (*func_ptr[9])(Node*) = {&print_revenue, &print_wholesale_cost, &print_current_investment,
		&print_total_profit, &print_total_sales, &print_average_profit, &print_items_in_stock,
		&print_items_out_of_stock, &print_by_string};
	void (*edit_func_ptr[3])(Node**,char*,int*) = {&add_item, &delete, &write_and_free_then_quit};
	Node *list_head_ptr = NULL;
	populate_list(&list_head_ptr,argv[1],argv[2],date);
	*option = 0;
	while(*option!=12){
		display_options(option);
		call_functions(func_ptr,edit_func_ptr,&list_head_ptr,argv,date,*option);		
	}
	return EXIT_SUCCESS;
}

