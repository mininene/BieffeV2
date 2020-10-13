#ifndef _TH4LIB_H
#define _TH4LIB_H

#define SS_VERSION         2.33   /* Version of this DLL */
#define CONNECT_TIMO       5000   /* Default time for the connection */
#define RECV_TIMO          2000   /* Default time for receiving a response */
#define SEND_TIMO          1000   /* Default time for sending a message */
#define VERSION_UNKNOWN      -1   /* Flag for marking that version unknown */

#define NUM_EXACT_PATHS      5 // Size of array of allowed paths that don't allow subdirectories (see sfi_pathIsAllowed)
#define NUM_PARENT_PATHS     3 // Size of array of allowed paths that do allow subdirectoris (see sfi_pathIsAllowed)

#pragma warning(disable: 4996)
#define DllImport   __declspec( dllimport )
#define DllExport   __declspec( dllexport )

extern unsigned long __stdcall th4_debug(long asl_debug);

extern unsigned long __stdcall th4_setup( long asl_conn_timeout, /* Connection timeout -1 (default) */
                         long asl_recv_timeout, /* Receive timeout -1 (default) */
                         long asl_send_timeout, /* Send timeout -1 (default) */
                         long asl_hour_delta,  /* Hour difference */
                         int asi_keepalive_interval, /* Keep alive interval */
                         int asi_keepalive_retries /* Keep alive retries */
                         );


extern  unsigned long __stdcall th4_initServer();

extern  long __stdcall th4_initSession(int asl_version, /* Version number eg. 15 for W15 */
                              char *apc_address);

extern  unsigned long __stdcall th4_connect(long asl_handle);

extern  unsigned long __stdcall th4_readFile(long asl_handle,/* Session handle */
                                             char *apc_remote_file,    /* Remote file name */
                                             char *apc_local_file      /* Optional local filename (can be NULL) */
                                             );

extern  unsigned long __stdcall th4_writeFile(long asl_handle,/* Session handle */
                                             char *apc_remote_file,    /* Remote file name */
                                             char *apc_local_file      /* Optional local filename (can be NULL) */
                                             );

extern  unsigned long __stdcall th4_close(long *apl_handle);

extern  unsigned long __stdcall th4_message(long asl_code, LPSTR* ppszString);

extern unsigned long __stdcall th4_software_version(long asl_handle, /* Session handle */
                                                    long *apl_version_id, /* Determine version id */
                                                    LPSTR* ppszFECP /* FECP version */
                                                    );

extern  unsigned long __stdcall th4_listDir(long asl_handle,
                                             char *apc_directory,
                                             char *apc_file,
                                             int si_options,
                                             int si_filter);

extern  unsigned long __stdcall th4_cwd(long asl_handle, char *apc_directory);

extern  unsigned long __stdcall th4_deleteFile(long asl_handle,
                                             char *apc_file);

extern  unsigned long __stdcall th4_copyFile(long asl_handle,
                                              char *apc_source,
                                              char *apc_dest);

extern  unsigned long __stdcall th4_renameFile(long asl_handle,
                                                char *apc_source,
                                                char *apc_dest);

extern  unsigned long __stdcall th4_existsFile(long asl_handle,
                                                char *apc_file,
                                               long *apl_exists);


extern  float __stdcall th4_version(void);

extern  unsigned long  __stdcall th4_lasterror(void);

extern unsigned long __stdcall th4_login(long asl_handle,
                                         char *apc_login);

extern int __stdcall th4_login_check(long asl_handle);


#endif