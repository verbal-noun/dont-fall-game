#include <stio.h>
#include <stdlib.h>

#define MAX 100

void print_array(int A[], int n)
{
    int i;
    for (i = 0; i < n; i++)
    {
        printf("%3d", A[i]);
    }
    printf("\n");
    return;
}

void int_swap(int *x, int *y)
{
    int tmp;
    tmp = *x;
    *x = *y;
    *y = tmp;
    return;
}

void print_perms(int n)
{
    int A[MAX], i, s, t;
    /* fill the array with the first permutation */
    for (i = 0; i < n; i++)
    {
        A[i] = i + 1;
    }
    while (1)
    {
        print_array(A, n);
        /* find the split point s after which array is decreasing */
        for (s = n - 1; s > 0 && A[s - 1] > A[s]; s--)
        {
            /* loop */
        }
        if (s == 0)
        {
            /* array is reverse sorted, so must have just
			   printed the last permutation and are all done */
            return;
        }
        /* now find smallest item in tail greater than A[s-1] */
        t = s;
        for (i = s + 1; i < n; i++)
        {
            if (A[i] > A[s - 1] && A[i] < A[t])
            {
                t = i;
            }
        }
        /* now swap A[s-1] and A[t] */
        int_swap(A + s - 1, A + t);
        /* and then reverse the whole tail to make it increasing */
        for (t = n - 1; s < t; s++, t--)
        {
            int_swap(A + s, A + t);
        }
        /* and that is now the next permutation! */
    }
}

/* simple driver program so that you can test the function :*/
int main(int argc, char **argv)
{
    if (argc > 1)
    {
        print_perms(atoi(argv[1]));
    }
    return 0;
}