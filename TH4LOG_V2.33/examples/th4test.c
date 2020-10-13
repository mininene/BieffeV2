/*
 * th4test.c : Simple C program to demonstrate the TH4LOG APIs
 * 
 * Written by : Graham Morris             Date: 30-May-2005
 *
 * Modification history:
 *   V1.00  GHM  30-May-2005 : First version
 *   V2.10  GHM  17-Mar-2006 : Rebuilt for W22
 */
#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <..\th4log\th4log.h>

#define SS_DIR_LIST "..dir.list"

static unsigned long sfm_view_dir(long asl_handle, char *apc_directory, char *apc_tab)
{
  char ac_dirfile[1024], ac_localname[1024];
  unsigned long sm_sts = 0;

  strncpy(ac_dirfile, apc_directory, sizeof(ac_dirfile));
  strncat(ac_dirfile, SS_DIR_LIST, sizeof(ac_dirfile));
  sm_sts = th4_listDir(asl_handle, apc_directory, ac_dirfile, 0, 0);
  /* If the listing was successful get the file */
  if (sm_sts == 0)
  {
    tmpnam(ac_localname);
    sm_sts = th4_readFile(asl_handle, ac_dirfile, ac_localname);
    if (sm_sts == 0)
    {
      FILE *px_ifile;

      px_ifile = fopen(ac_localname,"r");
      if (px_ifile == NULL)
      {
        printf("Unable to open file %s\n", ac_dirfile);
        return errno;
      }
      else
      {
        char ac_line[1024];

        while (!feof(px_ifile) && (sm_sts == 0))
        {
          ac_line[0] = '\0';
          fgets(ac_line, sizeof(ac_line)-1, px_ifile);
          /* There is a line and not the name of the directory file */
          if ((ac_line[0] != '\0') && (strncmp(ac_line, SS_DIR_LIST, strlen(SS_DIR_LIST)-1) != 0))
          {
            printf("%s%s", apc_tab, ac_line);
            // Found a directory 
            if ( strchr(ac_line, '\\') != NULL)
            {
              char ac_new_dir[1024];
              char ac_tab[100];

              /* Remove the \n */
              ac_line[strlen(ac_line)-1] = '\0';
              _snprintf(ac_new_dir, sizeof(ac_new_dir), "%s%s", apc_directory, ac_line);
              _snprintf(ac_tab, sizeof(ac_tab), "%s    ", apc_tab);

              sm_sts = sfm_view_dir(asl_handle, ac_new_dir, ac_tab);
            }
          }
        }
        fclose(px_ifile);
        remove(ac_localname);
      }
    }
    sm_sts = th4_deleteFile(asl_handle, ac_dirfile);
  }
  return(sm_sts);
}

/* Internal structure for relating the version numbers */
static struct dw_version_id 
{
  char *pc_name;
  long  sl_id;
}
sbx_version_id[] 
= {
"W12",   12,    
"W13",   13,
"W14",   14,
"W15",   15,
"W16",   16,
"W17",   17,
"W18",   18,
"W19",   19,
"W20",   20,
"W21",   21,
"W22",   22,
"W23",   23,
"W22.1", 2201,
"W18.1", 1801,
"W23.1", 2301,
"W23.2", 2302,
"W23.3", 2303,
"W23.4", 2304,
"W23.5", 2305,
"W24",   24,
"W24.1", 2401 ,
"W24.2", 2402 ,
"W25",   25,
"W26",   26,
"W27",   27,
"W27.1", 2701,
"W28",   28,
"W29",   29,
"W29.1", 2901,
"W29.2", 2902,
"W37",   37,
"Inquiry",-1, /* This is a special number that will inquire the version*/
NULL, 0 /* End marker*/
    };


int __cdecl main(int argc, char *argv[])
{
  unsigned long      sm_sts = 0;
  long               sl_handle;
  char               ac_buf[100];
  char               ac_version[20];
  char               ac_temp[20];
  char               ac_host[100];
  char               ac_remote[_MAX_FNAME];
  char               ac_local[_MAX_PATH];
  char               ac_login[_MAX_PATH];
  long               sl_version;
  LPSTR              sx_string = NULL;
  int                si_option;
  int                si_filter;
  int                si_count;
  int                si_lastcommand;
  long               sl_exists;
  int                si_debug = 0;

  ac_host[0] = '\0';
  ac_remote[0] = '\0';
  ac_local[0] = '\0';
  ac_login[0] = '\0';
  printf("TH4 Test Version 2.0\n");
  printf("Copyright(c) 2005. Fedegari SpA, Italy\n");
  printf("TH4Log DLL version: %4.2f\n\n", th4_version());
  while ( 1 )
  {
    /* Display any error messages ? */
    if ( sm_sts != 0 )
    {
      if (sx_string != NULL)
        sx_string[0] = '\0';
      th4_message(sm_sts, &sx_string);
      printf("Status = %d %lx '%s'\n", sm_sts, sm_sts, sx_string);
      sm_sts = 0;
    }
    printf("Select from the list below:\n");
    printf("   1=Initialise                     2=Connect\n"
           "   3=Getfile                        4=Sendfile\n"
           "   5=Close connection               6=Setup (defaults)\n"
           "   7=Debug                          8=Software Version\n"
           "   9=Change directory              10=Generate file list\n"
           "  11=Delete file                   12=Copy file\n"
           "  13=Rename file                   14=File exists\n"
           "  15=Recursive file list           16=Login\n"
           "  17=Loop test on last command     18=Connect test\n"
           "   0=Exit\n");
    do
    {
      printf("Select ? ");
      gets(ac_buf);
    }
    while (ac_buf[0] == '\0');

    switch ( atol(ac_buf) )
    {
      case 1:
        for (si_count = 0; (sbx_version_id[si_count].pc_name != NULL); si_count++)
          printf("%4d=%-5s    %c", si_count+1, sbx_version_id[si_count].pc_name, (si_count % 4 == 3) ? '\n' : ' ');
        printf("\n Version Number ? ");
        gets(ac_version);
        sl_version = atol(ac_version);
        if ((sl_version <= 0) || (sl_version > si_count))
        {
          printf("Invalid version selected\n");
        }
        else
        {
          sl_version = sbx_version_id[sl_version-1].sl_id;
          printf("Thema4 I/P Address/name ? ");
          gets(ac_host);
          // Initialise the session
          sl_handle = th4_initSession(sl_version, ac_host);
          if ( sl_handle == 0 )
            sm_sts = th4_lasterror();
        }
        break;

      case 2:
        sm_sts = th4_connect(sl_handle);
        break;

      case 3: // Get a file
        printf("Remote file ? ");
        gets(ac_remote);
        printf("Local file ? ");
        gets(ac_local);
        sm_sts = th4_readFile(sl_handle, ac_remote, ac_local);
        break;

      case 4: // Send a file
        printf("Remote file ? ");
        gets(ac_remote);
        printf("Local file ? ");
        gets(ac_local);
        sm_sts = th4_writeFile(sl_handle, ac_remote, ac_local);
        break;

      case 5: // Disconnect
        th4_close(&sl_handle);
        break;

      case 6: // Setup
        sm_sts = th4_setup(-1, -1, -1, 0, -1, -1);
        break;

      case 7: // Debug
        si_debug = !si_debug;
        sm_sts = th4_debug(si_debug);
        break;

      case 8: // Version information
        sm_sts = th4_software_version(sl_handle, &sl_version, &sx_string);
        if (sm_sts == 0)
        {
          printf("Information: %d %s\n", sl_version, sx_string);
        }
        break;

      case 9: // Change directory
        printf("Set current working directory ? ");
        gets(ac_remote);
        sm_sts = th4_cwd(sl_handle, ac_remote);
        break;

      case 10: // Generate a file list
        printf("List of directory ? ");
        gets(ac_remote);
        printf("Into file ? ");
        gets(ac_local);
        printf("Information 0=Brief, 1=Full ? ");
        si_option = atol(gets(ac_temp));
        printf("Filter date 0=None, -1=Since last last, +N=minutes since ? ");
        si_filter = atoi(gets(ac_temp));
        sm_sts = th4_listDir(sl_handle, ac_remote, ac_local, si_option, si_filter);
        /* If the listing was successful get the file */
        if (sm_sts == 0)
        {
          sm_sts = th4_readFile(sl_handle, ac_local, ac_local);
          if (sm_sts == 0)
          {
            FILE *px_ifile;
            int si_c;

            px_ifile = fopen(ac_local,"r");
            if (px_ifile == NULL)
            {
              printf("Unable to open file %s\n", ac_local);
            }
            else
            {
              while ((si_c = fgetc(px_ifile)) != EOF)
              {
                putc(si_c, stdout);
              }
              fclose(px_ifile);
            }
          }
        }
        break;

      case 11: // Delete file
        printf("File to delete ? ");
        gets(ac_remote);
        sm_sts = th4_deleteFile(sl_handle, ac_remote);
        break;

      case 12: // File copy
        printf("Source file ? ");
        gets(ac_remote);
        printf("Local file ? ");
        gets(ac_local);
        sm_sts = th4_copyFile(sl_handle, ac_remote, ac_local);
        break;

      case 13: // File rename
        printf("Source file ? ");
        gets(ac_remote);
        printf("Local file ? ");
        gets(ac_local);
        sm_sts = th4_renameFile(sl_handle, ac_remote, ac_local);
        break;

      case 14: // File exists
        printf("Source file ? ");
        gets(ac_remote);
        sm_sts = th4_existsFile(sl_handle, ac_remote, &sl_exists);
        printf("%s exists : %d\n", ac_remote, sl_exists);
        break;

      case 15: // Example of recursion down a sub-directory
        printf("Initial directory ? ");
        gets(ac_remote);
        sm_sts = sfm_view_dir(sl_handle, ac_remote, "");
        break;

      case 16: // Login
        printf("Password ? ");
        gets(ac_login);
        sm_sts = th4_login(sl_handle, ac_login);
        break;

      case 17:// Repeat last command
        printf("Loops ? ");
        si_count = atol(gets(ac_temp));
        while ((si_count-- > 0) && (sm_sts == 0))
        {
          switch (si_lastcommand)
          {
            case 3: // Read file
              sprintf(ac_temp, "%s_%d", ac_local, si_count);
              sm_sts = th4_readFile(sl_handle, ac_remote, ac_temp);
              break;

            case 4: // Send file
              sm_sts = th4_writeFile(sl_handle, ac_remote, ac_local);
              break;

            default:
              printf("Loop on command %d not support\n", si_lastcommand);
              si_count = -1;
              break;
          }
          Sleep(1000);
        }
        /* Do not set the last command */
        printf("All loops completed\n");
        continue;

      case 18:
        if ((ac_host[0] == '\0') || (ac_remote[0] == '\0') || (ac_local[0] == '\0'))
        {
          printf("Data not configured\n");
        }
        else
        {
          printf("Loops ? ");
          si_count = atol(gets(ac_temp));
          while (si_count-- > 0)
          {
            // Initialise the session
            sl_handle = th4_initSession(sl_version, ac_host);
            if ( sl_handle == 0 )
            {
              sm_sts = th4_lasterror();
              printf("Initialise fail %ld\n", sm_sts);
              break;
            }
            sm_sts = th4_connect(sl_handle);
            if (sm_sts != 0)
            {
              printf("Connection fail %ld\n", sm_sts);
              break;
            }
            sm_sts = th4_readFile(sl_handle, ac_remote, ac_local);
            if (sm_sts != 0)
            {
              printf("Read failed %ld\n", sm_sts);
            }
            th4_close(&sl_handle);
          }
        }
        break;

      case 0: // Exit
        exit(0);

      default:
        printf("Unknown command %s", ac_buf);
    }
    si_lastcommand = atol(ac_buf);
  }
}


