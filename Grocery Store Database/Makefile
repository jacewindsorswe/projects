# BY SUBMITTING THIS FILE TO CARMEN I CERTIFY I HAVE STRICTLY ADHERED
# TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
# 
# WRITTEN BY JACE WINDSOR 2/15/2022
#
CC = gcc
CFLAGS = -ansi -pedantic -Wimplicit-function-declaration -Wreturn-type -g
SFLAGS = -lc -m64 -g
OBJ = lab4.o parse_data.o populate_list.o insert.o confirm_continue.o calculate_days.o date_difference.o read_file.o delete_node.o print_item_info.o print_items_in_stock.o print_items_out_of_stock.o print_revenue.o print_wholesale_cost.o print_current_investment.o print_total_profit.o print_total_sales.o print_average_profit.c print_by_string.o add_item.o stock_num_exists.o delete.o write_and_free_then_quit.o display_options.o call_functions.o
all: lab4 lab4.zip

lab4.zip: *.h *.c Makefile lab4Readme
	zip $@ $^

%.o: %.c
	$(CC) -c $< -o $@ $(CFLAGS)

%.o: %.s
	$(CC) -c $< -o $@ $(SFLAGS)

lab4: $(OBJ)
	$(CC) $^ -o $@ $(CFLAGS)

clean:
	rm -rf *.o lab4 lab4.zip


