/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void call_functions(void (*func_ptr[])(Node *list_head_ptr),void (*edit_func_ptr[])(Node **list_head_ptr, char* argv, int *date),Node **list_head_ptr,char** argv, int* date,int option){
	switch(option){ /*handles functions that require edits (10-12) */
		case(10):	
		case(11):
		case(12):
			edit_func_ptr[(option-10)](list_head_ptr,argv[3],date);
			break;
		default: /*Handles 1-9 option */
			func_ptr[(option-1)](*list_head_ptr);
			break;
	}
}

