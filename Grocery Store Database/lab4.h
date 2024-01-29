/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/15/2022
 */

struct Cost{
	float wholesale_price;
	float retail_price;
	int wholesale_quantity;
	int retail_quantity;
};

struct Data{
	char item[50];
	char department[30];
	int stock_number;
	struct Cost pricing;
};

typedef struct Node{
	struct Data grocery_item;
	struct Node *next;
} Node;

/*sscanf all data in the line to a struct of data */
int parse_data(FILE *inventory, Node **new_node);

/*reads line by line and populates nodes into the list, returns the date array*/
void populate_list(Node **list_head_ptr, char* date, char* file, int* date_as_int);

/*Handles inserting node based on where the node needs to be inserted */
void insert(Node **list_head_ptr, Node *new_node_ptr);

/*Reads lines from file after checking if user wants to continue */
void read_file(Node **list_head_ptr, FILE *inventory);

/*Returns a y/n char if the user would like to continue after seeing the date */
char confirm_continue(char* date, int file_date[], int **date_as_int);

/*Calculates the argv date into an int usable in date_difference */
int calculate_days(int month, int day, int year);

/*Determines the number of days between the user date and file date */
int date_difference(int user_date[], int file_date[]);

/*prints the info of an item for the display in stock option */
void print_item_info(Node *item);

/*Prints all items with a positive quantity.*/
void print_items_in_stock(Node *list_head_ptr);

/*Prints all items out of stock */
void print_items_out_of_stock(Node *list_head_ptr);

/*prints the revenue of prodcts */
void print_revenue(Node *list_head_ptr);

/*prints the total wholesale cost */
void print_wholesale_cost(Node *list_head_ptr);

/*prints current investment (wholesale_price*(wholesale_quantity - retail_quantity */
void print_current_investment(Node *list_head_ptr);

/*Prints the total profit (wholesale cost*quantity) - cost */
void print_total_profit(Node *list_head_ptr);

/*Prints total number of sales */
void print_total_sales(Node *list_head_ptr);

/*Prints average profit per sale */
void print_average_profit(Node *list_head_ptr);

/*Prints all items that contain user entered substring in the department name */
void print_by_string(Node *list_head_ptr);

/*Prompots a user for the data to input then inserts into list if stock number doesnt exist */
void add_item(Node **list_head_ptr, char* dont_use, int* date_dont_use);

/*Checks if the user entered stock number alreadys appears in the list.*/
int stock_num_exists(Node **list_head_ptr, int stock_number);

/*Gets a stock number from the user then attemps to delete that item.*/
void delete(Node **list_head_ptr, char* dont_use, int* date_dont_use);

/*removes the node given by the user if it exists */
void delete_node(Node **list_head_ptr, int stock_number);

/*Writes inventory to file with updated date, frees memory, then exits */
void write_and_free_then_quit(Node **list_head_ptr,char* file_name,int *date);

/*Prints menu options */
void display_options(int* option);

/*Holds the switch/case calls for any option */
void call_functions(void (*func_ptr[])(Node *),void (*edit_func_ptr[])(Node **, char*, int*),Node **list_head_ptr,char** argv, int* date,int option);
