/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/23/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void populate_list(Node **list_head_ptr,char* date,char* file1,int *date_as_int){
	FILE *inventory;
	int file_date[2];
	char choice;
	inventory = fopen(file1,"r");
	fscanf(inventory,"%d\t%d\n",&file_date[0],&file_date[1]);
	choice = confirm_continue(date, file_date, &date_as_int);
	if(choice == 'y'||choice == 'Y'){
		read_file(list_head_ptr, inventory);
	}else{
		printf("User does not want to continue.");
	}
}

